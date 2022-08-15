"use strict";

const baseUrl = '/customers';
var dt;
var KTDatatablesServerSide = function () {
    var table;
    var initDatatable = function () {
        dt = $("#kt_datatable_example_1").DataTable({
            select: false,
            ajax: {
                method: "post",
                url: `${baseUrl}/getdata`,
                data: function (d) {
                    d['id'] = KTUtil.getURLParam('userId');
                },
            },
            columns: [
                { data: "id" },
                {
                    data: 'firstname',
                    sort: 'firstname',
                    render: function (data, type, row, meta) {
                        return `${row['firstname']} ${row['lastname']}`;
                    }
                },
                { data: 'phone' },
                { data: 'nationalcode' },
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
                        //html += `<a href="/transactions?userId=${data}" class="btn btn-sm btn-icon btn-info" data-bs-toggle="tooltip" title="تراکنش ها"><i class="bi bi-cash-stack"></i></a>`;
                        //html += `<a href="/usercards?userId=${data}" class="btn btn-sm btn-icon btn-info" data-bs-toggle="tooltip" title="کارت ها"><i class="bi bi-credit-card"></i></a>`;
                        html += "</div>";
                        return html;
                    }
                },
            ]
        });

        table = dt.$;
        dt.on('draw', function () {
            KTMenu.createInstances();
            KTApp.initBootstrapTooltips();
        });
        dt.on('ordering', function (d) {
            console.log(d);
        })
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