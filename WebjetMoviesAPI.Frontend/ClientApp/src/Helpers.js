

   export function Status(res) {
        if (!res.ok) {
            throw new Error(res.statusText);
        }
        return res;
    }

    export function AddDefaultSrc(ev) {
        ev.target.src = '/noposter.jpg';
    }
