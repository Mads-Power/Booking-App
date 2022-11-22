import { Container, Nav, Navbar as NavbarBs } from "react-bootstrap";
import { NavLink } from "react-router-dom";

export function Navbar() {
  return (
    <NavbarBs sticky="top" className="bg-white shadow-sm mb-3">
      <Container className="me-auto">
        <Nav>
          <Nav.Link to="/" as={NavLink}>
            Home
          </Nav.Link>
          <Nav.Link to="/list" as={NavLink}>
            List
          </Nav.Link>
        </Nav>
      </Container>
    </NavbarBs>
  );
}
