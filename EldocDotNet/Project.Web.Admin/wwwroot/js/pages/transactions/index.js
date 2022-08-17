"use strict";

var KTDatatablesServerSide = function () {
    const baseUrl = '/transactions';
    var dt;
    var table;
    const modalEl = document.getElementById("info_modal");
    modalEl.addEventListener('hidden.bs.modal', function (event) {
        dt.draw();
    })
    const info_modal = new bootstrap.Modal(modalEl);
    const modal_form = document.getElementById('modal_form');
    var initDatatable = function () {
        dt = $("#kt_datatable_example_1").DataTable({
            ordering: false,
            ajax: {
                method: "post",
                url: `${baseUrl}/getdata`,
                data: function (d) {
                    //$.each($("#dataTableSearchForm").serializeArray(), function (i, field) {
                    //    d[field.name] = field.value || "";
                    //});
                    d['userId'] = KTUtil.getURLParam('userId');
                },
            },
            columns: [
                { data: 'id' },
                { data: 'user' },
                {
                    data: 'amount',
                    render: function (data, type, row) {
                        return `<span class="text-${transactionType[row['transactionType']]['class']}">${KTUtil.numberString(data)}<small> ت</small></span>`;
                    }
                },
                {
                    data: 'paymentType',
                    render: function (data, type, row) {
                        return `<span class="badge badge-${transactionType[row['transactionType']]['class']}">${paymentType[data]['title']}</span>`;
                    }
                },
                { data: 'description' },
                {
                    data: 'updatedAt',
                    render: function (data, type, row) {
                        return `<span class="badge badge-dark">` + data.ToPersianDateString() + `</span>`;
                    }
                },
            ],
            createdRow: function (row, data, dataIndex) {
            }
        });

        table = dt.$;

        dt.on('draw', function () {
            KTMenu.createInstances();
            KTApp.initBootstrapTooltips();
        });
    }

    var addRecord = function () {
        const addButton = document.querySelector('[data-kt-docs-table-toolbar="add"]');

        addButton.addEventListener('click', function () {
            modal_form.reset();
            info_modal.show();
        });
    }

    var handleSearchDatatable = function () {
        const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            dt.search(e.target.value).draw();
        });
    }


    return {
        init: function () {
            initDatatable();
            handleSearchDatatable();
        }
    }

}();
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesServerSide.init();
});