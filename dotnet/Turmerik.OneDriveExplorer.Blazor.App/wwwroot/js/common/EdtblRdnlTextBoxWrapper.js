import { Trmrk as trmrk } from './core.js';
import { DomElWrapperBase } from './DomElWrapperBase.js';

export class EdtblRdnlTextBoxWrapper extends DomElWrapperBase {
    EditableTextBoxSelector;
    ReadonlyTextBoxSelector;

    __editableTextBox;
    __readonlyTextBox;
    __isEditable;

    constructor(
        domElId,
        editableTextBoxSelector,
        readonlyTextBoxSelector) {
        super(domElId || Trmrk.throw("Must specify parent dom element id"));
        this.EditableTextBoxSelector = editableTextBoxSelector || trmrk.throw("Must specify editable textbox selector");
        this.ReadonlyTextBoxSelector = readonlyTextBoxSelector || trmrk.throw("Must specify readonly textbox selector");
        this.__isEditable = !this.EditableTextBox.classList.contains("trmrk-hidden");
    }

    get IsEditable() {
        return this.__isEditable;
    }

    set IsEditable(value) {
        if (value) {
            this.ReadonlyTextBox.classList.add("trmrk-hidden");
            this.EditableTextBox.classList.remove("trmrk-hidden");
        } else {
            this.EditableTextBox.classList.add("trmrk-hidden");
            this.ReadonlyTextBox.classList.remove("trmrk-hidden");
        }

        this.__isEditable = value;
    }

    get EditableTextBox() {
        this.__editableTextBox = this.__editableTextBox || this.GetEditableTextBox();
        return this.__editableTextBox;
    }

    get ReadonlyTextBox() {
        this.__readonlyTextBox = this.__readonlyTextBox || this.GetReadonlyTextBox();
        return this.__readonlyTextBox;
    }

    GetEditableTextBox() {
        let editableTextBox = this.DomEl.querySelector(
            this.EditableTextBoxSelector);

        return editableTextBox;
    }

    GetReadonlyTextBox() {
        let readonlTextBox = this.DomEl.querySelector(
            this.ReadonlyTextBoxSelector);

        return readonlTextBox;
    }
}