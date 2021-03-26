// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var Site = function () {

  $.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px");
    this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");
    return this;
  }

  var blockUI = function() {
    $.blockUI({
      css: {
        backgroundColor: 'transparent',
        border: 'none'
      },
      message: '<div class="spinner-border" role="status"></span></div >',
      baseZ: 1500,
      overlayCSS: {
        backgroundColor: '#FFFFFF',
        opacity: 0.7,
        cursor: 'wait'
      }
    });
    $('.blockUI.blockMsg').center();
  }

  return {

    beforeRequest: function () {
      blockUI();
    },

    completeRequest: function() {
      $.unblockUI();
    }

  };

}();

