"use strict";

var KTDatatablesServerSide = function () {
    const baseUrl = '/requests';
    var dt;
    var table;
    const modalEl = document.getElementById("info_modal");
    modalEl.addEventListener('hidden.bs.modal', function (event) {
        dt.draw();
    })
    var initDatatable = function () {
        dt = $("#kt_datatable_example_1").DataTable({
            ordering: false,
            ajax: {
                method: "post",
                url: `${baseUrl}/getdata`,
                data: function (d) {
                    d['userId'] = KTUtil.getURLParam('userId');
                },
            },
            columns: [
                {
                    data: 'id',
                    render: function (data, type, row) {
                        return `<code>#${data}</code>`;
                    }
                },
                {
                    data: 'status',
                    render: function (data, type, row) {
                        return `<span class="badge badge-${chatWithExpertRequestStatus[data]['class']}">${chatWithExpertRequestStatus[data]['title']}</span>`;
                    }
                },
                { data: 'user' },
                {
                    data: 'sessionFee',
                    render: function (data, type, row) {
                        return `<span">${KTUtil.numberString(data)}<small> ت</small></span>`;
                    }
                },
                { data: 'description' },
                {
                    data: 'updatedAt',
                    render: function (data, type, row) {
                        return `<span class="badge badge-dark">` + data.ToPersianDateString() + `</span>`;
                    }
                },
                {
                    data: 'id',
                    orderable: false,
                    className: 'text-end',
                    render: function (data, type, row, meta) {
                        let html = '<div class="btn-group btn-group-sm px-2">';
                        if (row['status'] == 0) {
                            html += `<button class="btn btn-sm btn-icon btn-light-success" data-kt-docs-table-action="accept_row" data-bs-toggle="tooltip" title="تایید" data-id="${data}"><i class="bi bi-check"></i></button>`;
                            html += `<button class="btn btn-sm btn-icon btn-light-danger" data-kt-docs-table-action="reject_row" data-bs-toggle="tooltip" title="رد" data-id="${data}"><i class="bi bi-trash"></i></button>`;
                        }
                        html += "</div>";
                        return html;
                    }
                },
            ],
            createdRow: function (row, data, dataIndex) {
            }
        });

        table = dt.$;

        dt.on('draw', function () {
            handleActionRows();
            KTMenu.createInstances();
            KTApp.initBootstrapTooltips();
        });
    }

    var handleSearchDatatable = function () {
        const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            dt.search(e.target.value).draw();
        });
    }

    var handleActionRows = () => {
        const rejectButtons = document.querySelectorAll('[data-kt-docs-table-action="reject_row"]');
        const acceptButtons = document.querySelectorAll('[data-kt-docs-table-action="accept_row"]');

        rejectButtons.forEach(d => {
            d.addEventListener('click', function (e) {
                e.preventDefault();
                let id = e.target.closest('button').dataset.id;
                SwalConfirm.fire()
                    .then(function (result) {
                        if (result.value) {
                            fetch(`${baseUrl}/reject/${id}`)
                                .then(response => response.json())
                                .then(result => {
                                    if (result.status == 1) {
                                        toastr.success(result.message);
                                        dt.draw();
                                    } else {
                                        toastr.error(result.message)
                                    }
                                })
                                .catch(error => toastr.error("خطای سرور"));
                        }
                    });
            })
        });

        acceptButtons.forEach(d => {
            d.addEventListener('click', function (e) {
                e.preventDefault();
                let id = e.target.closest('button').dataset.id;
                SwalConfirm.fire()
                    .then(function (result) {
                        if (result.value) {
                            fetch(`${baseUrl}/accept/${id}`)
                                .then(response => response.json())
                                .then(result => {
                                    if (result.status == 1) {
                                        toastr.success(result.message);
                                        dt.draw();
                                    } else {
                                        toastr.error(result.message)
                                    }
                                })
                                .catch(error => toastr.error("خطای سرور"));
                        }
                    });
            })
        });
    }

    return {
        init: function () {
            initDatatable();
            handleSearchDatatable();
            handleActionRows();
            handleSearchDatatable();
        }
    }

}();
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesServerSide.init();
});