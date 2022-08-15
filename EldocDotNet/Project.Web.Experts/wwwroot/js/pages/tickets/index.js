"use strict";
const baseUrl = '/tickets';
var wrapper;

KTUtil.onDOMContentLoaded(function (e) {

    wrapper = new Vue({
        el: '#kt_content_container',
        data: {
            tickets: [],
            ticketMessages: [],
            currentTicket: {
                id: -1,
                priority: -1,
                status: -1,
                title: "",
                totalMessages: -1,
                updatedAt: "",
                updatedAtFormatted: "",
                user: "",
                userId: -1,
            },
            search: '',
            loadingMessages: false,
        },
        created: function () {
            this.GetTickets(this.search);
            this.SetRelativeTime();

            document.onkeydown = function (evt) {
                evt = evt || window.event;
                var isEscape = false;
                if ("key" in evt) {
                    isEscape = (evt.key === "Escape" || evt.key === "Esc");
                } else {
                    isEscape = (evt.keyCode === 27);
                }
                if (isEscape) {
                    wrapper.CloseChat();
                }
            };

            setTimeout(() => {
                let ie = document.getElementById('kt_chat_messenger_input');
                ie.addEventListener('keydown', function kk(e) {
                    if ((event.keyCode == 10 || event.keyCode == 13) && event.ctrlKey) {
                        wrapper.SendChatMessage();
                    }
                });
            }, 1234);
        },
        watch: {
            'search': function (val, oldVal) {
                wrapper.GetTickets(val);
            },
        },
        methods: {
            GetTickets: function (search) {
                fetch(`${baseUrl}/getdata?search=${search}`, { method: 'GET', })
                    .then(response => response.json())
                    .then(result => {
                        wrapper.tickets = result;
                        KTApp.initBootstrapTooltips();
                    })
                    .catch(error => toastr.error(error.message));
            },
            SetRelativeTime: function () {
                setInterval(function () {
                    document.querySelectorAll("span[data-refresh='yes']").forEach(element =>
                        element.innerHTML = moment(element.dataset.momentUpdate, "YYYY-MM-DDThh:mm:ss").locale('fa').fromNow()
                    );
                }, 1000);
            },
            ShowTicket: function (ticketId) {
                wrapper.CloseChat();
                this.loadingMessages = true;
                fetch(`${baseUrl}/details/${ticketId}`, { method: 'GET', })
                    .then(response => response.json())
                    .then(result => {
                        wrapper.currentTicket = result.details;
                        wrapper.ticketMessages = result.messages;
                        this.loadingMessages = false;
                    })
                    .then(() => {
                        KTUtil.scrollTo(document.getElementById('kt_chat_messenger'), 100, 456);
                        setTimeout(() => wrapper.ScrollChat(), 456);
                    })
                    .catch(error => toastr.error(error.message));
            },
            SendChatMessage: function () {
                let inputElement = document.getElementById('kt_chat_messenger_input');
                if (inputElement.value.length < 1) {
                    return;
                }

                let fd = new FormData();
                fd.append('ticketId', wrapper.currentTicket.id);
                fd.append('message', inputElement.value);
                fetch(`${baseUrl}/addmessage`, {
                    method: 'POST',
                    body: fd
                }).then(response => response.json())
                    .then(result => {
                        wrapper.ticketMessages.push(result);
                        if (wrapper.currentTicket.status == 0) {
                            wrapper.currentTicket.status = 2;
                            wrapper.tickets.find(f => f.id == wrapper.currentTicket.id).status = 2;
                        }
                    })
                    .then(() => {
                        wrapper.ScrollChat();
                    })
                    .catch(error => toastr.error(error.message));

                inputElement.value = "";
            },
            ScrollChat: function () {
                let chatElement = document.querySelector("#kt_chat_messenger");
                let messagesElement = chatElement.querySelector('[data-kt-element="messages"]');

                messagesElement.scrollTop = messagesElement.scrollHeight;
            },
            CloseChat: function () {
                wrapper.ticketMessages = [];
                wrapper.currentTicket = {
                    id: -1,
                    priority: -1,
                    status: -1,
                    title: "",
                    totalMessages: -1,
                    updatedAt: "",
                    updatedAtFormatted: "",
                    user: "",
                    userId: -1,
                };
            },
            CloseTicket: function () {
                SwalConfirm.fire()
                    .then(function (result) {
                        if (result.value) {
                            fetch(`${baseUrl}/close?id=${wrapper.currentTicket.id}`, { method: 'GET', })
                                .then(response => response.json())
                                .then(result => {
                                    console.log(result);
                                    wrapper.CloseChat();
                                    wrapper.GetTickets('');
                                })
                                .catch(error => toastr.error(error.message));
                        }
                    });

            }
        }
    });
});