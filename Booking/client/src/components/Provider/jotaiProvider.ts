import { atom } from 'jotai';
import { User } from '@type/user';

const placeholderUser: User = {
  name: '',
  email: '',
  phoneNumber: '',
  bookings: [],
};

export const dateAtom = atom<Date>(new Date(new Date().setHours(0,0,0,0)));


export const userAtom = atom<User>(placeholderUser);

// export const isAuthenticatedAtom = atom<boolean>(false);

