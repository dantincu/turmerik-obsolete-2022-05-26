export class DomHelper {
    constructor(domElId, selector) {
        if (!domElId && !selector) {
            throw new Error("Must specify either an id or a selector (or both )");
        }

        this.DomElId = domElId;
        this.Selector = selector;
    }

    DomElId;
    Selector;

    __domEl;

    get DomEl() {
        this.__domEl = this.__domEl || this.GetDomEl();
        return this.__domEl;
    }

    GetDomEl() {
        let domEl;

        if (this.DomElId) {
            domEl = document.getElementById(this.DomElId);
        } else {
            domEl = document;
        }

        if (this.Selector) {
            domEl = domEl.querySelector(this.Selector);
        }

        return domEl;
    }
}

const trmrk = {
    selectDomEl: (domElId, selector) => {
        let helper = new DomHelper(domElId, selector);
        helper.DomEl.select();
    },
    addCssClass: (domElId, selector, cssClass) => {
        let helper = new DomHelper(domElId, selector);
        helper.DomEl.classList.add(cssClass);
    },
    removeCssClass: (domElId, selector, cssClass) => {
        let helper = new DomHelper(domElId, selector);
        helper.DomEl.classList.remove(cssClass);
    },
    getDomElValue: (domElId, selector) => {
        let helper = new DomHelper(domElId, selector);
        return helper.DomEl.value;
    },
    getDomElInnerText: (domElId, selector) => {
        let helper = new DomHelper(domElId, selector);
        return helper.DomEl.innerText;
    },
    getDomElInnerHTML: (domElId, selector) => {
        let helper = new DomHelper(domElId, selector);
        return helper.DomEl.innerHTML;
    },
    getDomElOuterHTML: (domElId, selector) => {
        let helper = new DomHelper(domElId, selector);
        return helper.DomEl.outerHTML;
    }
};

export const Trmrk = trmrk;