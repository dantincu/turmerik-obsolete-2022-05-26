System.register([], function (exports_1, context_1) {
    "use strict";
    var trmrk, Trmrk;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            trmrk = {
                selectDomEl(selector) {
                    let domEl = document.querySelector(selector);
                    domEl.select();
                },
            };
            exports_1("Trmrk", Trmrk = trmrk);
        }
    };
});
