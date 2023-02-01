import { useQuery } from '@tanstack/react-query';
import { getMe } from './getMe';
import { User } from '../types/user'


const url = `/api/Account/IsAuthenticated`;
export const getIsAuthenticated = async (): Promise<Partial<User> | undefined> => {

  // Need to add undefined type here to avoid TS error
  let isSuccessful: Partial<User> | undefined = undefined;
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };

  const isAuthenticated = await fetch(url, requestOptions);

  if(isAuthenticated.ok || isAuthenticated.status == 200) {    
      const me = await getMe();
      return me;
  }

  return Promise.reject(new Error('Could not authenticate user'));
};

export const useIsAuthenticated = () => {
  return useQuery({
    queryKey: ['isAuthenticated'],
    queryFn: async () => await getIsAuthenticated(),
    retry: false
  });
};