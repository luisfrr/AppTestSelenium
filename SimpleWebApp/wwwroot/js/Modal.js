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
    CLOSE_MODAL: "btn btn-secondary"
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

  let config = {
    appModaSize: AppModalSizes.DEFAULT,
    url: "",
    title: "",
    bodyHtml: "",
    scroll: false,
    actions: [],
    onCreateModalFunction: function () { },
    onCloseModalFunction: function () { }
  };

  var initModal = function () {
    let config = {
      backdrop: true,
      keyboard: true,
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

    if (config.actions !== null && config.actions.length > 0)
      $(modalActions).removeClass().addClass('modal-footer');
    else
      $(modalActions).removeClass();

    config.actions.forEach(function(action) {
      if (action.type !== ModalTypeActions.CLOSE_MODAL)
        $(modalActions).append(`<button type="button" class="${action.type}" onclick="${action.onClickFunction}"> ${action.name} </button>`);
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
    if (config.url !== undefined || config.url !== null || config.url !== '') {

      $(modalBody).load(config.url, function (responseText, textStatus, req) {

        //console.log(responseText);
        //console.log(textStatus);
        //console.log(req);

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

        //console.log(responseText);
        //console.log(textStatus);
        //console.log(req);

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
    }

  };

}();
