import { useState } from "react";
import Button from "./components/Button";
import BookingForm from "./components/BookingForm";
import CheckIn from "./components/CheckIn";

function App() {
  return (
    <div className="App">
      <div>4/15</div>
      <CheckIn />
      <BookingForm />
    </div>
  );
}

export default App;
