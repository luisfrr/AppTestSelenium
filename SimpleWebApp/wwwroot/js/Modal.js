var ModalTypeActions = function () {

  return {
    PRIMARY: "btn btn-primary",
    SECONDARY: "btn btn-secondary",
    SUCCESS: "btn btn-success",
    DANGER: "btn btn-danger",
    WARNING: "btn btn-warning",
    INFO: "btn btn-info",
    LIGHT: "btn btn-light",
    DARK: "btn btn-dark",
    LINK: "btn btn-link",
    CLOSE_MODAL: "btn btn-secondary",
    CANCEL_CONFIRM_MODAL: "btn btn-danger"
  };

}();

var AppModalSizes = function () {

  return {
    SMALL: "modal-dialog modal-sm",
    DEFAULT: "modal-dialog",
    LARGE: "modal-dialog modal-lg",
    EXTRA_LARGE: "modal-dialog modal-xl",
    FULLSCREEN: "modal-dialog modal-fullscreen"
  };

}();

var AppModal = function () {

  var myModal = null;

  var modalSelector = "#app_modal ";
  var modalTitle = "#modal_title ";
  var modalBody = "#modal_body ";
  var modalConteiner = "div#modalSize ";
  var modalActions = "#modal_actions";
  var buttonModalClose = "#modal_close ";
  var actionModalClose = "#actionModalClose";
  var actionConfirmModalCancel = "#actionConfirmModalCancel";

  let config = {
    appModaSize: AppModalSizes.DEFAULT,
    url: "",
    title: "",
    message: "",
    bodyHtml: "",
    scroll: false,
    actions: [],
    onCreateModalFunction: function () { },
    onCloseModalFunction: function () { }
  };

  var initModal = function () {
    let config = {
      backdrop: 'static',
      keyboard: false,
      focus: true
    };

    myModal = new bootstrap.Modal(document.getElementById('app_modal'), config);
  };

  var setModalSize = function () {

    let appModalSize = config.appModaSize;

    if (appModalSize === AppModalSizes.SMALL)
      addClassModalSize(AppModalSizes.SMALL);
    else if (appModalSize === AppModalSizes.LARGE)
      addClassModalSize(AppModalSizes.LARGE);
    else if (appModalSize === AppModalSizes.EXTRA_LARGE)
      addClassModalSize(AppModalSizes.EXTRA_LARGE);
    else if (appModalSize === AppModalSizes.FULLSCREEN)
      addClassModalSize(AppModalSizes.FULLSCREEN);
    else {
      addClassModalSize(AppModalSizes.DEFAULT);
    }

  };

  var addClassModalSize = function (appModalSizes) {
    $(modalSelector + modalConteiner).removeClass().addClass(appModalSizes);
  }

  var setModalTitle = function () {
    $(modalTitle).html(config.title);
  };

  var setOnCloseFunction = function () {
    $(modalSelector + buttonModalClose).unbind();
    if (config.onCloseModalFunction !== null && typeof config.onCloseModalFunction === 'function') {
      $(buttonModalClose).on('click', config.onCloseModalFunction);
    }
  };

  var execOnCreateModalFunction = function () {
    if (config.onCreateModalFunction !== null && typeof config.onCreateModalFunction === 'function') {
      config.onCreateModalFunction();
    }
  };

  var setActions = function () {

    $(modalActions).html('');
    $(modalActions).removeClass('d-flex justify-content-center');

    if (config.actions !== null && config.actions.length > 0)
      $(modalActions).removeClass().addClass('modal-footer');
    else
      $(modalActions).removeClass();

    config.actions.forEach(function(action) {
      if (action.type !== ModalTypeActions.CLOSE_MODAL && action.type !== ModalTypeActions.CANCEL_CONFIRM_MODAL) {
        $(modalActions).append(`<button type="button" id="modalAction_${action.name}" class="${action.type}"> ${action.name} </button>`);
        $('#modalAction_' + action.name).unbind();
        if (action.onClickFunction !== null && typeof action.onClickFunction === 'function') {
          $('#modalAction_' + action.name).on('click', action.onClickFunction);
        }
      }
      else if (action.type == ModalTypeActions.CANCEL_CONFIRM_MODAL) {
        $(modalActions).append(`<button type="button" class="${action.type}" id="actionConfirmModalCancel" data-bs-dismiss="modal"> ${action.name} </button>`);

        $(actionConfirmModalCancel).unbind();
        if (config.onCloseModalFunction !== null && typeof config.onCloseModalFunction === 'function') {
          $(actionConfirmModalCancel).on('click', config.onCloseModalFunction);
        }
      }
      else {
        $(modalActions).append(`<button type="button" class="${action.type}" id="actionModalClose" data-bs-dismiss="modal"> ${action.name} </button>`);

        $(actionModalClose).unbind();
        if (config.onCloseModalFunction !== null && typeof config.onCloseModalFunction === 'function') {
          $(actionModalClose).on('click', config.onCloseModalFunction);
        }
      }
    });
  };

  var setScrollable = function () {
    if (config.scroll)
      $(modalSelector + modalConteiner).addClass('modal-dialog-scrollable');
  };

  var loadModalBody = function () {
    if (config.url != undefined) {

      $(modalBody).load(config.url, function (responseText, textStatus, req) {
        
        if (textStatus !== 'error') {
          setOnCloseFunction();
          setScrollable();
          setActions();
          execOnCreateModalFunction();
        }
        else {
          Toast.error('Error', 'Something went wrong when trying to open the modal. <br/> Details:' + responseText);
        }

      });

    }
    else {

      $(modalBody).html(config.bodyHtml).promise().done(function (responseText, textStatus, req) {

        if (textStatus !== 'error') {
          setOnCloseFunction();
          setScrollable();
          setActions();
          execOnCreateModalFunction();
        }
        else {
          Toast.error('Error', 'Something went wrong when trying to open the modal. <br/> Details:' + responseText);
        }

      });
    }

  };

  return {

    show: function (options) {

      if (options !== null)
        config = options;

      setModalSize();
      setModalTitle();
      loadModalBody();
      initModal();

      myModal.show();
    },

    hide: function () {
      myModal.hide();
    },

    confirmModal: function (options) {
      var bodyHtml = `
        <div class="row">
          <div class="col-md-12 text-center confirm-body">
            <i class="fa fa-exclamation-triangle fa-3x text-warning mb-3"></i>
            <h5><b>${options.title}</b></h5>
            <p>${options.message}</p>
          </div>
        </div>
      `;

      let optionsConfirm = {
        appModaSize: options.appModaSize,
        title: "",
        bodyHtml: bodyHtml,
        scroll: true,
        actions: options.actions,
        onCreateModalFunction: options.onCreateModalFunction,
        onCloseModalFunction: options.onCloseModalFunction
      }

      if (optionsConfirm !== null)
        config = optionsConfirm;

      setModalSize();
      setModalTitle();
      loadModalBody();
      initModal();

      $(modalActions).addClass('d-flex justify-content-center');

      myModal.show();
    }

  };

}();
