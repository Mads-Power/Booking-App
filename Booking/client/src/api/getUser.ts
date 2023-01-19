import { useQuery } from '@tanstack/react-query';
import { User } from '@type/user';

const url = '/api/User/';
export const getUser = async (id: string) => {
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  const res = await fetch(url + id, requestOptions);
  return res.json();
};

export const useUser = (id: string) => {
  return useQuery<User>({
    queryKey: ['user', id],
    queryFn: () => getUser(id),
    enabled: !!id
  });
};
