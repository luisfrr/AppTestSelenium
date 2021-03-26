var AdPreviewModal = function () {

  var UrlController = "/Widgets/Summernote/";
  var containerSelector = "#PreviewConteiner ";

  var AdPreview = function () {

    $.ajax({
      type: "GET",
      url: UrlController + "GetAddConfig",
      dataType: "JSON",
      beforeSend: function () { Site.beforeRequest() },
      complete: function () { Site.completeRequest() },
      error: function (jqXHR, textStatus, errorThrown) { },
      success: function (response) {

        if (response.status === ResponseStatus.ERROR) {
          Toast.error(response.status, response.message + '<br/>' + response.details);
        } else {
          if (response.data.editor !== null && response.data.editor !== '') {
            $(containerSelector).html(response.data.editor);
          }
        }

      }
    });

  };

  return {

    init: function () {
      AdPreview();
    }

  }

}();
