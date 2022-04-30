const trmrk = {
  selectDomEl(selector: string) {
    let domEl: any = document.querySelector(selector);
    domEl.select();
  },
};

export const Trmrk = trmrk;
