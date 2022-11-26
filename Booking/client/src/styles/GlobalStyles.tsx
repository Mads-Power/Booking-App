import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
  align-items: center;
  position: relative;
  min-height: 10rem;
  gap: 1em;
  border-radius: 5px;
  border: 2px solid #cecece;
  background-color: #f9f9f9;

  &:hover {
    border: 2px solid #54a4d1;
  }

  @media screen and (max-width: 600px) {
    .column {
      width: 100%;
    }
    &:hover {
      border: 2px solid #54a4d1;
    }
  }
`;

export const ContainerMain = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  position: relative;
  min-height: 10rem;
  gap: 1em;
`;
