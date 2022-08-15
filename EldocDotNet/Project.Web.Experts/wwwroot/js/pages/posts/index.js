"use strict";

const baseUrl = '/posts';
var dt;
var KTDatatablesServerSide = function () {
    var table;
    var initDatatable = function () {
        dt = $("#kt_datatable_example_1").DataTable({
            select: {
                style: 'multi',
                selector: 'td:first-child input[type="checkbox"]',
                className: 'row-selected'
            },
            ajax: {
                method: "post",
                url: `${baseUrl}/getdata`,
            },
            columns: [
                {
                    data: 'id',
                    orderable: false,
                    render: function (data) {
                        return `
                            <div class="form-check form-check-sm form-check-custom form-check-solid">
                                <input class="form-check-input" type="checkbox" value="${data}" />
                            </div>`;
                    }
                },
                { data: 'title' },
                { data: 'postCategory' },
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
                        html += `<a href="${baseUrl}/upsert/${data}" class="btn btn-sm btn-icon btn-info" data-bs-toggle="tooltip" title="ویرایش" data-index="${meta.row}"><i class="bi bi-pencil-square"></i></a>`;
                        html += `<button class="btn btn-sm btn-icon btn-light-danger" data-kt-docs-table-action="delete_row" data-bs-toggle="tooltip" title="رد" data-id="${data}"><i class="bi bi-trash"></i></button>`;
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
            initToggleToolbar();
            toggleToolbars();
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
        const deleteButtons = document.querySelectorAll('[data-kt-docs-table-action="delete_row"]');

        deleteButtons.forEach(d => {
            d.addEventListener('click', function (e) {
                e.preventDefault();
                let id = e.target.closest('button').dataset.id;
                SwalConfirm.fire()
                    .then(function (result) {
                        if (result.value) {
                            fetch(`${baseUrl}/delete/${id}`)
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

    var handleDeselectRows = () => {
        const resetButton = document.querySelector('[data-kt-docs-table-select="deselect"]');
        const container = document.querySelector('#kt_datatable_example_1');
        resetButton.addEventListener('click', function () {
            const headerCheckbox = container.querySelectorAll('[type="checkbox"]')[0];
            headerCheckbox.checked = false;
            dt.draw();
        });
    }

    // Init toggle toolbar
    var initToggleToolbar = function () {
        const container = document.querySelector('#kt_datatable_example_1');
        const checkboxes = container.querySelectorAll('[type="checkbox"]');

        const deleteSelected = document.querySelector('[data-kt-docs-table-select="delete_selected"]');
        const progressBar = document.querySelector('[data-kt-docs-table-progress="progressbar"]');
        const progressBarContainer = document.querySelector('[data-kt-docs-table-progress="base"]');

        checkboxes.forEach(c => {
            c.addEventListener('click', function () {
                setTimeout(function () {
                    toggleToolbars();
                }, 50);
            });
        });

        deleteSelected.addEventListener('click', function () {
            SwalConfirm.fire()
                .then(function (result) {
                    if (result.value) {
                        progressBarContainer.classList.remove('d-none');
                        let counter = 0;
                        let totalItems = dt.rows({ selected: true }).data().count();
                        progressBar.style.width = `${parseInt(counter / totalItems * 100)}%`;
                        progressBar.innerHTML = `${parseInt(counter / totalItems * 100)}%`;

                        let promises = [];
                        for (let i = 0; i < totalItems; i++) {
                            promises.push(
                                fetch(`${baseUrl}/delete/${dt.rows({ selected: true }).data()[i].id}`)
                                    .then(response => response.json())
                                    .then(result => {
                                        counter++;
                                        progressBar.style.width = `${parseInt(counter / totalItems * 100)}%`;
                                        progressBar.innerHTML = `${parseInt(counter / totalItems * 100)}%`;
                                    })
                                    .catch(error => toastr.error("خطای سرور"))
                            );
                        }
                        Promise.all(promises)
                            .then(function handleData(data) {
                                dt.draw();
                                setTimeout(() => {
                                    progressBarContainer.classList.add('d-none');
                                    toastr.success('عملیات انجام شد');
                                }, 543);
                            })
                            .catch(error => toastr.error("خطای سرور"));
                    }
                });

        });
    }

    // Toggle toolbars
    var toggleToolbars = function () {
        // Define variables
        const container = document.querySelector('#kt_datatable_example_1');
        const toolbarBase = document.querySelector('[data-kt-docs-table-toolbar="base"]');
        const toolbarSelected = document.querySelector('[data-kt-docs-table-toolbar="selected"]');
        const selectedCount = document.querySelector('[data-kt-docs-table-select="selected_count"]');

        // Select refreshed checkbox DOM elements
        const allCheckboxes = container.querySelectorAll('tbody [type="checkbox"]');

        // Detect checkboxes state & count
        let checkedState = false;
        let count = 0;

        // Count checked boxes
        allCheckboxes.forEach(c => {
            if (c.checked) {
                checkedState = true;
                count++;
            }
        });

        // Toggle toolbars
        if (checkedState) {
            selectedCount.innerHTML = count;
            toolbarBase.classList.add('d-none');
            toolbarSelected.classList.remove('d-none');
        } else {
            toolbarBase.classList.remove('d-none');
            toolbarSelected.classList.add('d-none');
        }
    }

    return {
        init: function () {
            initDatatable();
            handleSearchDatatable();
            initToggleToolbar();
            handleActionRows();
            handleDeselectRows();
        }
    }
}();

KTUtil.onDOMContentLoaded(function () {
    KTDatatablesServerSide.init();
});