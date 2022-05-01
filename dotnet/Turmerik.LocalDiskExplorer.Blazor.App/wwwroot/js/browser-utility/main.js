import { Trmrk as trmrk } from './core.js';
import { DomHelper } from './DomHelper.js';
import { EdtblRdnlTextBoxWrapper } from './EdtblRdnlTextBoxWrapper.js';

trmrk.selectDomEl = (domElId, selector) => {
    let helper = new DomHelper(domElId, selector);
    helper.DomEl.select();
};

trmrk.addCssClass = (domElId, selector, cssClass) => {
    let helper = new DomHelper(domElId, selector);
    helper.DomEl.classList.add(cssClass);
};

trmrk.removeCssClass = (domElId, selector, cssClass) => {
    let helper = new DomHelper(domElId, selector);
    helper.DomEl.classList.remove(cssClass);
};

trmrk.getDomElValue = (domElId, selector) => {
    let helper = new DomHelper(domElId, selector);
    return helper.DomEl.value;
};

trmrk.getDomElInnerText = (domElId, selector) => {
    let helper = new DomHelper(domElId, selector);
    return helper.DomEl.innerText;
};

trmrk.getDomElInnerHTML = (domElId, selector) => {
    let helper = new DomHelper(domElId, selector);
    return helper.DomEl.innerHTML;
};

trmrk.getDomElOuterHTML = (domElId, selector) => {
    let helper = new DomHelper(domElId, selector);
    return helper.DomEl.outerHTML;
};

trmrk.textBoxWrapperSetEditable = (
    domElId,
    editableTextBoxSelector,
    readonlyTextBoxSelector,
    isEditable) => {
    let wrapper = new EdtblRdnlTextBoxWrapper(
        domElId,
        editableTextBoxSelector,
        readonlyTextBoxSelector);

    if (isEditable) {
        wrapper.EditableTextBox.value = wrapper.ReadonlyTextBox.value;
    } else {
        wrapper.ReadonlyTextBox.value = wrapper.EditableTextBox.value;
    }

    wrapper.IsEditable = isEditable;

    if (isEditable) {
        wrapper.EditableTextBox.select();
    }

    return wrapper.EditableTextBox.value;
};

export const Trmrk = trmrk;