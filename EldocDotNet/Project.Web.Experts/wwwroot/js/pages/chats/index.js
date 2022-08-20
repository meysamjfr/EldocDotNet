"use strict";
const baseUrl = '/chat';
var wrapper;

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .withAutomaticReconnect()
    .build();

KTUtil.onDOMContentLoaded(function (e) {
    wrapper = new Vue({
        el: '#chat_items_container',
        data: {
            chatItems: [],
            loadigItems: false,
            currentChatUser: '',
            currentChatId: 0,
            currentChatMessages: [],
        },
        created: function () {
            this.GetChats();
        },
        methods: {
            GetChats: function () {
                fetch(`${baseUrl}/getdata`)
                    .then(response => response.json())
                    .then(result => {
                        wrapper.chatItems = result;
                    })
                    .catch(error => toastr.error(error.message));
            },
            GetMessages: function (id, user) {
                messagesContainerBlockUI.block();
                if (wrapper.currentChatId != 0) {
                    connection.invoke("Leave", wrapper.currentChatId);
                }
                connection.invoke("Join", wrapper.currentChatId);

                $('a[data-currentchatuser]').text(user);
                this.currentChatUser = user;
                this.currentChatId = id;
                fetch(`${baseUrl}/getmessages?id=${id}`)
                    .then(response => response.json())
                    .then(result => {
                        wrapper.currentChatMessages = result;
                        wrapper.InitiateChatMessages();
                        wrapper.SetCurrentChatRead();
                    })
                    .finally(() => {
                        messagesContainerBlockUI.release();
                        messagesContainer.scrollTop = messagesContainer.scrollHeight;
                    });

            },
            SetCurrentChatRead: function () {
                fetch(`${baseUrl}/setread?id=${wrapper.currentChatId}`);
            },
            FinishCurrentChat: function () {
                SwalConfirm.fire()
                    .then(function (result) {
                        if (result.value) {
                            fetch(`${baseUrl}/finishchat?id=${wrapper.currentChatId}`)
                                .then(response => response.json())
                                .catch(error => toastr.error("خطای سرور"))
                                .finally(() => {
                                    wrapper.GetChats();
                                    $('#kt_drawer_chat_close').trigger('click');
                                });
                        }
                    });
            },
            InitiateChatMessages: function () {
                messagesContainer.innerHTML = '';
                var messageWrapper = '';
                this.currentChatMessages.forEach((message) => {
                    if (message.isUser) {
                        messageWrapper = otherMessage(message, wrapper.currentChatUser);
                    } else {
                        messageWrapper = myMessage(message);
                    }
                    messagesContainer.insertAdjacentHTML('beforeend', messageWrapper);
                });
            }
        }
    });

    connection.start().then(function () {
        _handleSendMessage();
    }).catch(function (err) {
        return console.log(err);
    });

    connection.onreconnecting(error => {
        console.assert(connection.state === signalR.HubConnectionState.Reconnecting);
        handleLogMessage(`Connection lost due to error "${error}". Reconnecting.`);
    });

    const messagesContainer = document.querySelector('#kt_drawer_chat_messenger_body > div');
    const messagesContainerBlockUI = new KTBlockUI(messagesContainer, { 'message': 'در حال دریافت اطلاعات' });

    const messagesFooterContainer = document.querySelector('#kt_drawer_chat_messenger_footer');
    const messagesFooterContainerBlockUI = new KTBlockUI(messagesFooterContainer, { 'message': 'در حال ارسال پیام' });

    var myMessage = function (message) {
        let content = message.content.replaceAll('\n', '<br/>');
        return `<div class="d-flex justify-content-end mb-10">
                    <div class="d-flex flex-column align-items-end">
                        <div class="d-flex align-items-center mb-2">
                            <div class="me-3">
                                <span class="text-muted fs-7 mb-1">${message.updatedAtFormatted}</span>
                                <a href="javascript:;" class="fs-5 fw-bolder text-gray-900 text-hover-primary ms-1">کارشناس</a>
                            </div>
                            <div class="symbol symbol-35px symbol-circle">
                                <img alt="Pic" src="assets/media/avatars/blank.png" />
                            </div>
                        </div>
                        <div class="p-5 rounded bg-light-primary text-dark fw-bold mw-lg-400px text-end" data-kt-element="message-text">${content}</div>
                    </div>
                </div>`;
    }

    var otherMessage = function (message, user) {
        let content = message.content.replaceAll('\n', '<br/>');
        return `<div class="d-flex justify-content-start mb-10">
                    <div class="d-flex flex-column align-items-start">
                        <div class="d-flex align-items-center mb-2">
                            <div class="symbol symbol-75px symbol-circle">
                                <span class="symbol-label bg-light-danger text-danger fs-5 fw-bolder">${user.substring(0, 1)}</span>
                            </div>
                            <div class="ms-3">
                                <a href="javascript:;" class="fs-5 fw-bolder text-gray-900 text-hover-primary me-1">${user}</a>
                                <span class="text-muted fs-7 mb-1">${message.updatedAtFormatted}</span>
                            </div>
                        </div>
                        <div class="p-5 rounded bg-light-info text-dark fw-bold mw-lg-400px text-start" data-kt-element="message-text">${content}</div>
                    </div>
                </div>`;
    }

    var _handleSendMessage = function () {
        $('button[data-kt-element="send"]').on('click', () => {
            let messageContent = $('textarea[data-kt-element="input"]').val();
            if (messageContent == null || messageContent == '') return;
            messagesFooterContainerBlockUI.block();
            $('textarea[data-kt-element="input"]').val('');
            connection
                .invoke("SendMessageByExpert", wrapper.currentChatId, messageContent)
                .then(() => {
                    messagesFooterContainerBlockUI.release();
                    messagesContainer.scrollTop = messagesContainer.scrollHeight;
                })
                .catch(function (err) {
                    return console.error(err.toString());
                });
        });
    }

    connection.on("ReceiveMyMessage", function (group, message) {
        messagesContainer.insertAdjacentHTML('beforeend', myMessage(message));
    });

    connection.on("ReceiveMessage", function (group, message) {
        messagesContainer.insertAdjacentHTML('beforeend', otherMessage(message, wrapper.currentChatUser));
    });

});