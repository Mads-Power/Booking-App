import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  justify-content: center;
  gap: 1em;
  flex-wrap: wrap;
  align-items: center;
  min-height: 10rem;
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
  min-height: 10rem;
  gap: 1em;
`;

export const TestContainer = styled.div`
  display: grid;

  grid-template-columns: (4, 1fr);
  justify-content: space-evenly;
  grid-template-rows: auto;
  grid-template-areas:
    s "main main . sidebar"
    "footer footer footer footer";

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
