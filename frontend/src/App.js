import React, { useState } from "react";
import PrivateRoute from "./routes/PrivateRoute";
import ProtectedRoute from "./routes/ProtectedRoute";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home/Home";
import Auth from "./pages/Auth/Auth";
import Logout from "./pages/Auth/Logout";
import Layout from "./components/Layout/SideNavbarLayout";
import ItemDetails from "./pages/ItemDetails/ItemDetails";
import { AuthProvider } from "./context/AuthContext";
import NewItem from "./pages/NewItem/NewItem";
import InactiveItems from "./pages/InactiveItems/InactiveItems";
import ChooseWinner from "./pages/ChooseWinner/ChooseWinner";
import YourWins from "./pages/YourWins/YourWins";

function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          <Route
            path="/auth"
            element={
              <ProtectedRoute>
                <Auth />
              </ProtectedRoute>
            }
          />
          <Route
            path="/logout"
            element={
              <PrivateRoute>
                <Logout />
              </PrivateRoute>
            }
          />

          <Route path="/" element={<Layout />}>
            <Route
              index
              element={
                <PrivateRoute>
                  <Home />
                </PrivateRoute>
              }
            />
            <Route
              path="new-item"
              element={
                <PrivateRoute>
                  <NewItem />
                </PrivateRoute>
              }
            />
            <Route
              path="inactive-items"
              element={
                <PrivateRoute>
                  <InactiveItems />
                </PrivateRoute>
              }
            />
            <Route
              path="lottery/:id"
              element={
                <PrivateRoute>
                  <ItemDetails />
                </PrivateRoute>
              }
            />
            <Route
              path="choose-winner"
              element={
                <PrivateRoute>
                  <ChooseWinner />
                </PrivateRoute>
              }
            />
            <Route
              path="your-wins"
              element={
                <PrivateRoute>
                  <YourWins />
                </PrivateRoute>
              }
            />
          </Route>
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}

export default App;
