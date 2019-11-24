import React, { Component } from 'react';

export class Error extends Component {
    static displayName = Error.name;

    render() {
        return (
            <h1 className="text-danger">Page not found.</h1>
        );
    }
}
