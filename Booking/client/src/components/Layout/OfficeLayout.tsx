import React from "react";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  min-height: 10rem;
  gap: 1em;
`;

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
        <ChooseOfficeButton>OSLO</ChooseOfficeButton>
        <ChooseOfficeButton>DRAMMEN</ChooseOfficeButton>
      </Container>
    </>
  );
};

export default OfficeLayout;
