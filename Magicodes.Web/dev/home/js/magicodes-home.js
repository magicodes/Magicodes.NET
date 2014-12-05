/* ========================================
 * Magicodes Home Front-end JavaScript
 * ver. 0.0.100
 * by taojiachun
 * ======================================== */




/* ========================================
 * Homepage
 * 页面淡入
 * ======================================== */

+function ($) {
  'use strict';
  
  $(".m-home").animate({opacity: "1"}, 1000);
  
}(jQuery);




/* ========================================
 * 通用页面
 * fixed footer-sole
 * ======================================== */

+function ($) {
  'use strict';
  
  // 当footer-sole为fixed时，使其在页面底部与之前内容的距离如同非fixed一样高。
  if ($(".footer-sole").css("position") === "fixed") {
    
    var fPH, fsPH;
    
    // footer中container的padding-bottom值
    fPH = $(".footer").children(".container").css("padding-bottom").slice(0, $(".footer").children(".container").css("padding-bottom").length - 2);
    
    // footer-sole的高度
    fsPH = $(".footer-sole").outerHeight();
    
    // 将footer-sole的高度加到footer的padding-bootm上去
    $(".footer").children(".container").css("padding-bottom", Number(fPH) + Number(fsPH) + "px");

  }
  
}(jQuery);




/* ========================================
 * Homepage
 * 顶部 topbar & 底部 footer 背景变色
 * ======================================== */

+function ($) {
  'use strict';
  
  $(window).scroll(function () {
    
    /* topbar */
    if ($(window).scrollTop() > 30) {
      $(".topbar").css("background", "rgba(0, 0, 0, 0.8)");
    }
    if ($(window).scrollTop() < 30) {
      $(".topbar").css("background", "none");
    }
    
    /* footer */    
    if ($("body").hasClass("m-home")) {
      if ($(window).scrollTop() > 1) {
        $(".footer-sole").css("background", "rgba(0, 0, 0, 0.8)");
      }
      if ($(window).scrollTop() < 1) {
        $(".footer-sole").css("background", "none");
      }
    }
    
  });

}(jQuery);




/* ========================================
 * Homepage
 * 巨幕 face
 * ======================================== */

+function ($) {
  'use strict';
  
  $(".face").css("height", window.innerHeight);
  
}(jQuery);




/* ========================================
 * Homepage
 * m-introduce 介绍文字切换
 * ======================================== */

+function ($) {
  'use strict';
  
  var bTitle, bText, fTitle, fText, $thisTitle, $thisText;
  
  /* 后端 */
  bTitle = $("#intro-back").find(".intro-title");
  bText = $("#intro-back").find(".intro-text");
  
  /* 前端 */
  fTitle = $("#intro-front").find(".intro-title");
  fText = $("#intro-front").find(".intro-text");
  
  bTitle.mouseup(function () {
    
    var bId;
    $thisTitle = $(this);
    bId = $thisTitle.data("btitleid");

    bText.each(function () {
      $thisText = $(this);
      if ($thisText.data("btextid") === bId) {
        $thisText.slideDown(1000);
      } else {
        $thisText.slideUp(800);
      }
    });

  });
  
  fTitle.mouseup(function () {
    
    var fId;
    $thisTitle = $(this);
    fId = $thisTitle.data("ftitleid");

    fText.each(function () {
      $thisText = $(this);
      if ($thisText.data("ftextid") === fId) {
        $thisText.slideDown(1000);
      } else {
        $thisText.slideUp(800);
      }
    });

  });
  
}(jQuery);




/* ========================================
 * 通常页面
 * 侧边 aside
 * ======================================== */

+function ($) {
  'use strict';
  
  if ($('.aside').length > 0) {
    
    // 侧边栏菜单的展开与收缩

    var $as, $asLa;
    $as = $(".aside");
    $asLa = $as.children("ul").children("li").children("a");

    $asLa.each(function() {
      $(this).click(function () {
        $(this).siblings("ul").slideToggle();
      });
    });
    
  }
  
  if ($('.m-page-main').length > 0) {

    // 页面滚下时自动固定；滚上去自动解锁

    var asTop;
    asTop = $(".m-page-main").offset().top - 40;

    $(window).scroll(function () {
      if ($(window).scrollTop() > asTop) {
        $as.css("margin-top", -asTop);
        $as.css("position", "fixed");
      }
      if ($(window).scrollTop() < asTop) {
        $as.css("margin-top", 0);
        $as.css("position", "static");
      }
    });
    
  }
  
  

  
}(jQuery);




/* ========================================
 * 注册和登录页
 * register & login
 * ======================================== */

+function ($) {
  'use strict';
  
  var rEmail, rUser, rPswd1, rPswd2, rAccept;
  rEmail = $("#register").find("#InputEmail");
  rUser = $("#register").find("#InputUsername");
  rPswd1 = $("#register").find("#InputPassword1");
  rPswd2 = $("#register").find("#InputPassword2");
  rAccept = $("#register").find("#AcceptRegisterLicense");
  
//  console.log(rEmail.val(), rUser.val(), rPswd1.val(), rPswd2.val(), rAccept.attr("checked"));

  
  // 判断用户名字数
  
  rUser.focusout(function () {
    if (rUser.val().length < 1 && rUser.next(".m-alert").length === 0) {
      rUser.parent().append('<div class="m-alert alert alert-warning alert-dismissible" role="alert"><button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>用户名字数必须大于2</div>');
    }
    if (rUser.val().length >= 1) {
      rUser.next(".m-alert").remove();
    }
    // （还要加非法字符判断）
  });
  
  
  // 判断两个密码是否相同
  rPswd2.focusout(function () {
    if (rPswd2.val() !== rPswd1.val() && rPswd2.next(".m-alert").length === 0) {
      rPswd2.parent().append('<div class="m-alert alert alert-warning alert-dismissible" role="alert"><button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>两次输入的密码不同</div>');
    }
    if (rPswd2.val() === rPswd1.val()) {
      rPswd2.next(".m-alert").remove();
    }
  });
  
  
}(jQuery);




/* ========================================
 * Homepage
 * 响应式功能
 * ======================================== */

+function ($) {
  'use strict';
  
  // 小屏菜单
  
  $('.top-menu-button-r').click(function () {
    $('.top-menu-list').slideToggle('fast');
  });
  
  $('.top-user-button-r').click(function () {
    $('.top-user-menu').slideToggle('fast');
  });

}(jQuery);







