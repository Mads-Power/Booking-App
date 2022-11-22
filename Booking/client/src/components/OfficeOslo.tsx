import styled from "styled-components";

const Seats = styled.button`
  padding: 10px;
  border: 2px solid blue;
  border-radius: 4px;

  @media screen and (max-width: 45em) {
    padding: 1rem 1rem;
    font-size: 1.5rem;
    margin: 0.5rem;
  }
`;

const OfficeOslo = () => {
  return (
    <div>
      <Seats>1</Seats>
      <Seats>2</Seats>
      <Seats>3</Seats>
      <Seats>4</Seats>
      <Seats>5</Seats>
      <Seats>6</Seats>
      <Seats>7</Seats>
      <Seats>8</Seats>
      <Seats>9</Seats>
      <Seats>10</Seats>
    </div>
  );
};

export default OfficeOslo;
