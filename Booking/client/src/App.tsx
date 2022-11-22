import Home from "./components/Home";
import { Routes, Route } from "react-router-dom";
import { Navbar } from "./components/NavBar";
import { Container } from "react-bootstrap";
import OfficeLayout from "./components/Layout/OfficeLayout";
import OfficeOslo from "./components/OfficeOslo";

function App() {
  return (
    <div>
      <Navbar />
      <Container className="mb-4">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/OfficeLayout" element={<OfficeLayout />} />
          <Route path="/officeOslo" element={<OfficeOslo />} />
        </Routes>
      </Container>
    </div>
  );
}

export default App;
