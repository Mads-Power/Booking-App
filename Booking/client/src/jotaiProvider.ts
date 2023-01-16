import { atom } from 'jotai';

const defaultUser = {
  id: 5,
  name: 'Default User',
  email: 'default@user.com',
  phoneNumber: '01234567',
  bookings: [
    {
      id: 24,
      seatId: 5,
      userId: 5,
      date: '2023-01-03T12:00:00+01:00',
    },
  ],
};

export const dateAtom = atom(new Date(new Date().setHours(0,0,0,0)));
