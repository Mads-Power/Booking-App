import { useQuery } from '@tanstack/react-query';
import { User } from '@type/user';

const url = 'http://localhost:51249/api/User/';
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

// Placeholder is used on initial load
const placeholderUser: User = {
  id: 0,
  name: '',
  email: '',
  phoneNumber: '',
  bookings: [],
};

export const useUserQuery = (id: string) => {
  return useQuery<User>({
    queryKey: ['user', id],
    queryFn: () => getUser(id),
    enabled: !!id,
    placeholderData: placeholderUser,
  });
};
