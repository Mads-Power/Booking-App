import { atom } from 'jotai';

export const dateAtom = atom(new Date(new Date().setHours(0,0,0,0)));
