import { Container, Nav, Navbar as NavbarBs } from "react-bootstrap";
import Button from "react-bootstrap/Button";
import { NavLink } from "react-router-dom";
import styled from "styled-components";

export function Navbar() {
  return (
    <NavbarBs sticky="top" className="bg-white shadow-sm mb-3">
      <Container className="me-auto">
        <Nav>
          <Nav.Link to="/" as={NavLink}>
            Home
          </Nav.Link>
          <Nav.Link to="/OfficeLayout" as={NavLink}>
            Booke plass
          </Nav.Link>
        </Nav>
      </Container>
    </NavbarBs>
  );
}
