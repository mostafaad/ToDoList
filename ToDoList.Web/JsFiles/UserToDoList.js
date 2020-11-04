////////////Data Binding/////////////////////
var DatatableDataSources = function () {

    // Basic Datatable examples
    var _componentDatatableDataSources = function () {
        if (!$().DataTable) {
            console.warn('Warning - datatables.min.js is not loaded.');
            return;
        }

        // Setting datatable defaults
        $.extend($.fn.dataTable.defaults, {
            autoWidth: false,
            dom: '<"datatable-header"fBl><"datatable-scroll-wrap"t><"datatable-footer"ip>',
            buttons: [
                {
                    extend: 'print',
                    text: '<i class="icon-printer mr-2"></i> طباعة',
                    className: 'btn bg-blue',

                    customize: function (win) {
                        $(win.document.body).css('direction', 'rtl');
                    }
                }
            ],
            language: {
                search: '<span>Filter:</span> _INPUT_',
                searchPlaceholder: 'Type to filter...',
                lengthMenu: '<span>Show:</span> _MENU_',
                paginate: { 'first': 'First', 'last': 'Last', 'next': $('html').attr('dir') === 'rtl' ? '&larr;' : '&rarr;', 'previous': $('html').attr('dir') === 'rtl' ? '&rarr;' : '&larr;' }
            }
        });

        $.ajax({
            url: '/UserToDoList/GetAllUserToDoList'

        }).done((data) => {

            $('.datatable-js').dataTable().fnDestroy();

            $('.datatable-js').dataTable({
                data: data,
                responsive: true,
                retrieve: true,

                columns: [

                    { 'data': 'Title' },
                    { 'data': 'date' },
                   
                    {
                        'data': 'ID',
                        'searchable': false,
                        'sortable': false,
                        'render': (id) => {

                            return '<div class="list-icons"><div class="dropdown"><a href="#" class="list-icons-item" data-toggle="dropdown">' +
                                ' <i class="icon-menu9"></i></a> <div class="dropdown-menu dropdown-menu-right">' +
                                '  <a href="Update/' + id + '" class="dropdown-item btn-edit"><i class="icon-file-pdf"></i> تعديل</a>' +
                                
                                ' <a href="javascript:void(0)" onclick="return Delete(' + id + ')" class="dropdown-item"><i class="icon-file-excel"></i> حذف</a>'

                                ' </div> </div>  </div>'

                        }
                    }

                ]
            });

        }).fail((e) => {
            toastr.error(e.responseText, 'ERROR');
        })
    };

    //
    // Return objects assigned to module
    //

    return {
        init: function () {
            _componentDatatableDataSources();

        }
    }
}();



//////////////////////////////////////////////////////////////////////////////////////
function onsuccess(data) {
    if (data.data === "Add") {
        if (data.Success === true) {
            $('#myModalContent').modal('hide');
            localStorage.setItem("success", "تم حفظ البيانات بنجاح")

            window.location = window.location.origin + '/UserToDoList/List'

        }
        else {
            new Noty({
                text: localStorage.getItem("success"),
                type: 'error'
            }).show();
        }
    }

    if (data.data === "Edit") {
        if (data.Success === true) {
            localStorage.setItem("success", "تم حفظ البيانات بنجاح")

            window.location = window.location.origin + '/UserToDoList/List'

        }
        else {
            new Noty({
                text: localStorage.getItem("success"),
                type: 'error'
            }).show();
        }
    }
}
/////////////////////////////////////////////////////////////////////////////////////////////////
function Delete(id) {

    swal({
        title: 'هل متأكد من الحذف',
        text: "لن تستطيع التراجع بعد الحذف",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'نعم',
        cancelButtonText: 'الغاء الامر',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false
    }).then(function () {

        $.ajax({
            type: "post",
            url: '/UserToDoList/Delete',
            data: {
                id: id
            },
            success: function (data) {

                DatatableDataSources.init();
            }

        })
        swal(
            'تنويه',
            'تم الحذف بنجاح',
            'success'
        );
    }, function (dismiss) {

        // Dismiss can be 'cancel', 'overlay', 'close', and 'timer'
        if (dismiss === 'cancel') {

            swal(
                'Cancelled',
                'Your imaginary file is safe :)',
                'error'
            );
        }
    });


}