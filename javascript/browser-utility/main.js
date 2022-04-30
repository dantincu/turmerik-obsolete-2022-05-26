const trmkr = {
    selectSelector: (selector) => {
        let domEl = document.querySelector(selector);
        domEl.select();
    }
};

export const Trmrk = trmkr;