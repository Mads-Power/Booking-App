import React, { useState } from 'react';
import Button from './Button';
import { useForm, SubmitHandler } from 'react-hook-form';
import listOfPeople from './../data/list-of-people.json';

const BookingForm = () => {
  const [members, setMembers] = useState(listOfPeople.developers);
  return (
    <form action=''>
      <input placeholder='Søk...' type='text' />
      {members.map(member => (
        <Button name={member.name} key={member.id} />
      ))}
    </form>
  );
};

export default BookingForm;
