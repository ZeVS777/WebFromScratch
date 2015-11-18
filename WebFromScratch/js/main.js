/*
 * Загружаемые шрифты. Смотри https://github.com/typekit/webfontloader
 */

WebFontConfig = {
    google: { families: ['Lobster::latin,cyrillic'] }
};
(function () {
    var wf = document.createElement('script');
    wf.src = ('https:' == document.location.protocol ? 'https' : 'http') +
      '://ajax.googleapis.com/ajax/libs/webfont/1/webfont.js';
    wf.type = 'text/javascript';
    wf.async = 'true';
    var s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(wf, s);
})();