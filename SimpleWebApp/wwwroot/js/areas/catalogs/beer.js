var Beer = function () {
  var UrlController = "/Catalogs/Beer/";
  var gridSelector = "#gridBeer ";
  var validatorSelector = "#BeerValidator";
  var $grid = null;

  var configTable = {
    bServerSide: true,
    bFilter: false,
    ordering: true,
    info: false,
    processing: true,
    scrollX: true,
    scrollY: 450,
    paging: false,
    ajax: {
      url: UrlController + 'GetListBeers',
      type: 'POST',
      dataSrc: 'data',
      dataType: 'json',
      data: function (d) {

        var filters = {
          Name: $('#divFilters #NameFilter').val(),
          Brand: $('#divFilters #BrandFilter').val()
        };

        d.filters = filters;

        return d;
      },
    },
    columns: [
      {
        data: "id",
        autoWidth: true,
        title: "id",
        visible: false
      },
      {
        data: "brand",
        autoWidth: true,
        title: "Brand",
      },
      {
        data: "name",
        autoWidth: true,
        title: "Name",
      },
      {
        data: "alcohol",
        autoWidth: true,
        title: "Alcohol",
      },
      {
        title: "Actions",
        action: 0,
        autoWidth: true,
        bSearchable: false,
        bSortable: false,
        class: "text-center dt-center td-actions",
        render: function (data, row, meta) {
          var html = '';

          html += '<a href="javascript: Beer.OpenModalForm(' + meta.id + ');" class="btn btn-primary btn-outline btn-sm" title="Edit"><span class="fa fa-pencil"></span></a>';

          html += '<a href="javascript: Beer.ConfirmDelete(' + meta.id + ');" class="btn btn-danger btn-outline btn-sm" title="Delete" style="margin-left:16px !important;"><span class="fa fa-trash"></span></a>';

          return html;
        }
      }
    ]
  };

  var Save = function () {

    var form = $('#BeerForm')[0];
    var formData = new FormData(form);

    $.ajax({
      url: UrlController + 'Save',
      data: formData,
      processData: false,
      contentType: false,
      dataType: 'JSON',
      type: 'POST',
      beforeSend: function () { Site.beforeRequest(); },
      complete: function () { Site.completeRequest(); Beer.Search(); },
      error: function (error) { console.log(error); Toast.error('error', error.status + ' - ' + error.statusText); },
      success: function (response) {

        if (response.status === ResponseStatus.ERROR) {
          Toast.error(response.status, response.message + '<br/>' + response.details);
        }
        else if (response.status === ResponseStatus.FATAL_ERROR) {
          Toast.fatalError(response.status, response.message + '<br/>' + response.details);
        } else if (response.status === ResponseStatus.WARNING) {
          Site.showValidatorErrors(validatorSelector, response.data);
          return;
        } else if (response.status === ResponseStatus.SUCCESS) {
          Toast.success(response.status, response.message);
        }

        AppModal.hide();        
      }
    });
  };

  var Delete = function (id) {

    $.ajax({
      url: UrlController + 'Delete/' + id,
      processData: false,
      contentType: false,
      type: 'POST',
      beforeSend: function () { Site.beforeRequest(); },
      complete: function () { Site.completeRequest(); Beer.Search(); },
      error: function (error) { Toast.error('error', error.status + ' - ' + error.statusText); AppModal.hide(); },
      success: function (response) {

        if (response.status === ResponseStatus.ERROR) {
          Toast.error(response.status, response.message + '<br/>' + response.details);
        }
        else if (response.status === ResponseStatus.FATAL_ERROR) {
          Toast.fatalError(response.status, response.message + '<br/>' + response.details);
        } else {
          Toast.success(response.status, response.message);
        }

        AppModal.hide();
      }
    });

  };

  return {

    Init: function () {
      $grid = $(gridSelector).DataTable(configTable);
      $('table.table thead tr th').addClass('table-light');
    },

    Search: function () {
      $grid.draw();
    },

    ClearFilter: function () {
      $('#divFilters #NameFilter').val('');
      $('#divFilters #BrandFilter').val('');
    },

    OpenModalForm: function (id) {
      let params = (id === null || id === 0) ? '' : '?id=' + id;
      let title = (id === null || id === 0) ? 'New Beer' : 'Edit Beer';

      let options = {
        appModaSize: AppModalSizes.DEFAULT,
        url: UrlController + 'Form' + params,
        title: title,
        bodyHtml: "",
        scroll: true,
        actions: [
          {
            name: "Cancel",
            type: ModalTypeActions.CLOSE_MODAL
          },
          {
            name: "Save",
            type: ModalTypeActions.SUCCESS,
            onClickFunction: function () { Save() }
          }
        ],
        onCreateModalFunction: function () {  },
        onCloseModalFunction: function () {  }
      }

      AppModal.show(options);
    },

    ConfirmDelete: function (id) {

      let options = {
        appModaSize: AppModalSizes.SMALL,
        title: 'Confirm Action',
        message: 'Are you sure you want to delete this?',
        actions: [
          {
            name: 'Cancel',
            type: ModalTypeActions.CANCEL_CONFIRM_MODAL
          },
          {
            name: 'Yes',
            type: ModalTypeActions.SUCCESS,
            onClickFunction: function () { Delete(id); }
          },
        ],
        onCreateModalFunction: function () { },
        onCloseModalFunction: function () { }
      }

      AppModal.confirmModal(options);
    }

  };

}();



$(document).ready(function () {

  Beer.Init();

});
