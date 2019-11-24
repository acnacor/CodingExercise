import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Movie } from './components/Movie';
import { Error } from './components/Error';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Switch>
            <Route exact path='/' component={Home} />
            <Route path='/movie/:id' component={Movie} />
                <Route path='*' component={Error} />
            </Switch>
      </Layout>
    );
  }
}
