import { useQuery } from '@tanstack/react-query';
import { atom, useAtom } from 'jotai'
import { atomsWithQuery } from 'jotai-tanstack-query'

const url = `/api/User/Me`;
export const getMe = async () => {
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  return await fetch(url, requestOptions);
};