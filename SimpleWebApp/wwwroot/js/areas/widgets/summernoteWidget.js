var SummernoteWidget = function () {
  var UrlController = "/Widgets/Summernote/";

  return {

    init: function () {
      SummernoteWidget.loadPlugging();
      SummernoteWidget.GetAdConfig();
    },

    loadPlugging: function() {

      // Summernote Pluggin

      let toolbarOps = [
        // [groupName, [list of button]]
        ['style', ['style', 'bold', 'italic', 'underline', 'clear']],
        ['font', ['strikethrough', 'superscript', 'subscript']],
        ['fontsize', ['fontsize']],
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['height', ['height']],
        ['table', ['table']],
        ['insert', ['link', 'picture', 'video']],
        ['view', ['help']]
      ];

      let summernoteOps = {
        height: 300,          // set editor height
        minHeight: null,      // set minimum height of editor
        maxHeight: null,      // set maximum height of editor
        focus: true,          // set focus to editable area after initializing summernote
        tabsize: 2,           // set tab size
        lang: 'en-US',        // default 'en-US', other 'es-ES'
        placeholder: "Type somenthing...",
        toolbar: toolbarOps
      }

      $('#editor').summernote(summernoteOps);

      // End Summernote Pluggin
    },

    SaveAdConfing: function() {
      debugger;
      var editor = $('#editor').summernote('code');

      var data = { editor };

      $.ajax({
        type: "POST",
        url: UrlController + "SaveConfig",
        dataType: "JSON",
        data: data,
        beforeSend: function () { Site.beforeRequest() },
        complete: function () { Site.completeRequest() },
        error: function (jqXHR, textStatus, errorThrown) { },
        success: function (response) {

          if (response.status === ResponseStatus.SUCCESS) {
            Toast.success(response.status, response.message);
          } else if (response.status === ResponseStatus.WARNING) {
            Toast.warning(response.status, response.message + '<br/>' + response.details);
          } else {
            Toast.error(response.status, response.message + '<br/>' + response.details);
          }

        }
      });

    },

    GetAdConfig: function () {

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
            Toast.success(response.status, response.message);

            if (response.data.editor !== null && response.data.editor !== '') {
              $('#editor').summernote('reset');
              $('#editor').summernote('pasteHTML', response.data.editor);
            }
          }
          
        }
      });

      
    },

    OpenPreview: function () {

      let options = {
        appModaSize: AppModalSizes.LARGE,
        url: UrlController + 'AdPreview',
        title: "Preview",
        bodyHtml: "",
        scroll: true,
        actions: [
          //{
          //  name: "Close",
          //  type: ModalTypeActions.CLOSE_MODAL
          //}
        ],
        onCreateModalFunction: function () { AdPreviewModal.init(); },
        onCloseModalFunction: function () { /*SummernoteWidget.GetAdConfig();*/ }
      }

      AppModal.show(options);
    }
  };

}();


$(document).ready(function () {
  SummernoteWidget.init();
});
