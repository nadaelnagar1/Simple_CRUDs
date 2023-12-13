import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import { Home } from './components/Home'; // Import the Home component
import { ToastContainer } from 'react-toastify'; // Import ToastContainer
import './custom.css';
import 'react-toastify/dist/ReactToastify.css'; // Ensure you import the CSS for react-toastify

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <ToastContainer /> {/* Place ToastContainer at the root */}
        <Routes>
          {/* Map your routes */}
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
          {/* Add a specific route for the Home component */}
          <Route path="/home" element={<Home />} />
        </Routes>
      </Layout>
    );
  }
}
