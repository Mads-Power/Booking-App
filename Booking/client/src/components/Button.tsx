import React from 'react';

type Props = {
  name: string;
  onClick?: () => void;
};

const Button = ({ name, onClick }: Props) => {
  return (
    <li onClick={onClick}>
      <p>{name}</p>
    </li>
  );
};

export default Button;
