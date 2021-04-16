var ResponseStatus = function () {

  return {

    SUCCESS: "success",
    ERROR: "error",
    WARNING: "warning",
    FATAL_ERROR: "fatal error",
    INFO: "info"
  };

}();

var Toast = function () {
  let toastSelector = '#appToast';
  let toastHeader = '#toastHeader';
  let titleSelector = '#toastTitle';
  let iconSelector = '#toastIcon';
  let timeSelector = '#toastTime';
  let messageSelector = '#toastMessage';

  var initToast = function (animation, autohide, delay) {

    var option = {
      animation: animation,
      autohide: autohide,
      delay: delay
    };

    var toastElList = [].slice.call(document.querySelectorAll('.toast'))
    var toastList = toastElList.map(function (toastEl) {
      // Creates an array of toasts (it only initializes them)
      return new bootstrap.Toast(toastEl, option) // No need for options; use the default options
    });
    toastList.forEach(toast => toast.show()); // This show them
  }
  return {

    success: function (title, message, autoHide = true) {
      $(toastHeader).removeClass().addClass('toast-header bg-success');
      $(iconSelector).removeClass().addClass('fa fa-check text-white');
      $(titleSelector).html(title.toUpperCase()).removeClass().addClass("me-auto text-white");
      $(timeSelector).html('');
      $(messageSelector).html(message);

      initToast(true, autoHide, 5000);
    },

    error: function (title, message, autoHide = false) {
      $(toastHeader).removeClass().addClass('toast-header bg-danger');
      $(iconSelector).removeClass().addClass('fa fa-exclamation text-white');
      $(titleSelector).html(title.toUpperCase()).removeClass().addClass("me-auto text-white");
      $(timeSelector).html('');
      $(messageSelector).html(message);

      initToast(true, autoHide, 20000);
    },

    warning: function (title, message, autoHide = true) {
      $(toastHeader).removeClass().addClass('toast-header bg-warning');
      $(iconSelector).removeClass().addClass('fa fa-exclamation-triangle text-dark');
      $(titleSelector).html(title.toUpperCase()).removeClass().addClass("me-auto text-dark");
      $(timeSelector).html('');
      $(messageSelector).html(message);

      initToast(true, autoHide, 10000);
    },

    fatalError: function (title, message, autoHide = false) {
      $(toastHeader).removeClass().addClass('toast-header bg-danger');
      $(iconSelector).removeClass().addClass('fa fa-bug text-white');
      $(titleSelector).html(title.toUpperCase()).removeClass().addClass("me-auto text-white");
      $(timeSelector).html('');
      $(messageSelector).html(message);

      initToast(true, autoHide, 20000);
    },

    info: function (title, message, autoHide = true) {
      $(toastHeader).removeClass().addClass('toast-header bg-info');
      $(iconSelector).removeClass().addClass('fa fa-info text-dark');
      $(titleSelector).html(title.toUpperCase()).removeClass().addClass("me-auto text-dark");
      $(timeSelector).html('');
      $(messageSelector).html(message);

      initToast(true, autoHide, 15000);
    },

    hide: function () {
      $(toastSelector).toast('hide');
    },

    dispose: function () {
      $(toastSelector).toast('dispose');
    }

  };

}();

