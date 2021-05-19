import React, { Component } from 'react';
import AuthContext from '../auth/AuthProvider';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <>
        <AuthContext>
          {this.props.children}
        </AuthContext>
      </>
    );
  }
}
