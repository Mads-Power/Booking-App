import List from "./components/List";
import Home from "./components/Home";
import { Routes, Route } from "react-router-dom";
import { Navbar } from "./components/NavBar";
import { Container } from "react-bootstrap";

function App() {
  return (
    <div>
      <Navbar />
      <Container className="mb-4">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/list" element={<List />} />
        </Routes>
      </Container>
    </div>
  );
}

export default App;
