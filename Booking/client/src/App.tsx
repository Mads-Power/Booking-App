import { useState } from "react";
import Button from "./components/Button";
import BookingForm from "./components/BookingForm";
import CheckIn from "./components/Home";
import List from "./components/List";
import Home from "./components/Home";
import { Routes, Route } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/list" element={<List />} />
      </Routes>
    </div>
  );
}

export default App;
