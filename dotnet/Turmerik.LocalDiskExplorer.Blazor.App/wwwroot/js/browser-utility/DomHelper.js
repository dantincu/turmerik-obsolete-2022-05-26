import { DomElWrapperBase } from './DomElWrapperBase.js';

export class DomHelper extends DomElWrapperBase {
    Selector;

    constructor(domElId, selector) {
        super(domElId);

        if (!domElId && !selector) {
            throw new Error("Must specify either an id or a selector (or both )");
        }

        this.Selector = selector;
    }

    GetDomEl() {
        let domEl = super.GetDomEl() || document;

        if (this.Selector) {
            domEl = domEl.querySelector(this.Selector);
        }

        return domEl;
    }
}