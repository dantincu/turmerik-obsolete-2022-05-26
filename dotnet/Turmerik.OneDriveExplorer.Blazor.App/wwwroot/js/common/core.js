const trmrk = {
    throw: (err) => {
        if (typeof (err) !== "object") {
            err = new Error(err);
        }

        throw err;
    }
}

export const Trmrk = trmrk;