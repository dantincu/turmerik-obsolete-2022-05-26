import { Trmrk as trmrk } from './core.js';
import { DomHelper } from './DomHelper.js';
import { EdtblRdnlTextBoxWrapper } from './EdtblRdnlTextBoxWrapper.js';

trmrk.selectDomEl = (domElId, selector) => {
    let helper = new DomHelper(domElId, selector);
    helper.DomEl.select();

    return helper.DomEl.value;
};

trmrk.textToLines = (text, maxLineLen) => {
    let lines = [];
    let textLen = text.length;

    let linesCount = Math.ceil(textLen / maxLineLen);

    let stIdx = 0;
    let endIdx = maxLineLen - 1;

    for (let i = 0; i < linesCount; i++) {
        lines[i] = text.substring(stIdx, endIdx);

        stIdx = endIdx;
        endIdx += maxLineLen;
    }

    return lines;
}

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
    isEditable,
    revertChanges) => {
    let wrapper = new EdtblRdnlTextBoxWrapper(
        domElId,
        editableTextBoxSelector,
        readonlyTextBoxSelector);

    wrapper.IsEditable = isEditable;

    if (isEditable || revertChanges) {
        wrapper.EditableTextBox.value = wrapper.ReadonlyTextBox.value;

        if (isEditable) {
            wrapper.EditableTextBox.select();
        };
    } else {
        wrapper.ReadonlyTextBox.value = wrapper.EditableTextBox.value;
    }

    return wrapper.EditableTextBox.value;
};

trmrk.webStorage = {
    bigItems: {},
    getItem: (key, isPersistent) => {
        let storage = trmrk.webStorage.getStorage(isPersistent);
        let retVal = storage.getItem(key);

        return retVal;
    },
    setItem: (key, value, isPersistent) => {
        let storage = trmrk.webStorage.getStorage(isPersistent);
        storage.setItem(key, value);
    },
    getStorage: (isPersistent) => {
        let storage;

        if (isPersistent) {
            storage = localStorage;
        } else {
            storage = sessionStorage;
        }

        return storage;
    },
    getBigItemChunksCount: (key, guidStr, maxChunkLength, isPersistent) => {
        let text = trmrk.webStorage.getItem(key, isPersistent);

        if (typeof (text) !== "string") {
            text = "";
        }

        let lines = trmrk.textToLines(text, maxChunkLength);
        trmrk.webStorage.bigItems[guidStr] = lines;

        let chunksCount = lines.length;
        return chunksCount;
    },
    getBigItemChunk: (guidStr, idx) => {
        let textChunk = trmrk.webStorage.bigItems[guidStr][idx];
        return textChunk;
    },
    clearBigItemChunks: guidStr => {
        delete trmrk.webStorage.bigItems[guidStr];
    }
}

export const Trmrk = trmrk;