import React from "react";
import styled from "styled-components";
import { Container } from "../../styles/GlobalStyles";
Container;
import { Link } from "react-router-dom";

const ChooseOfficeButton = styled.button`
  padding: 10px;
  border: 2px solid blue;
  border-radius: 4px;

  @media screen and (max-width: 45em) {
    padding: 1rem 1rem;
    font-size: 1.5rem;
    margin: 0.5rem;
  }
`;

const OfficeLayout = () => {
  return (
    <>
      <Container>
        <ChooseOfficeButton>
          <Link to="/officeOslo">OSLO</Link>
        </ChooseOfficeButton>
        <ChooseOfficeButton>DRAMMEN</ChooseOfficeButton>
      </Container>
    </>
  );
};

export default OfficeLayout;
