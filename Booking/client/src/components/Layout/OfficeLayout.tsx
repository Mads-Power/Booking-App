import React from "react";
import styled from "styled-components";
import {
  Container,
  ContainerMain,
  TestContainer,
} from "../../styles/GlobalStyles";
Container;
import { Link } from "react-router-dom";

const ChooseOfficeButton = styled.button`
  padding: 30px 61px;
  border: none;
  background: none;
  cursor: pointer;
  margin: 0;
  padding: 0;
`;

const OfficeLayout = () => {
  return (
    <>
      <TestContainer>
        <Link to="/officeOslo">
          <ChooseOfficeButton>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            OSLO
          </ChooseOfficeButton>
        </Link>

        <ChooseOfficeButton>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          DRAMMEN
        </ChooseOfficeButton>
      </TestContainer>
    </>
  );
};

export default OfficeLayout;
