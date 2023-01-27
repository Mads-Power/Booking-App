import { useQuery } from '@tanstack/react-query';
import { User } from '@type/user';
import { atom, useAtom } from 'jotai'
import { atomsWithQuery } from 'jotai-tanstack-query'

const url = `/api/User/`;
export const getUser = async (email: string) => {
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  const res = await fetch(url + email, requestOptions);
  return res.json();
};

// Placeholder is used on initial load
const placeholderUser: User = {
  name: '',
  email: '',
  phoneNumber: '',
  bookings: [],
};

export const useUserQuery = (email: string) => {
  return useQuery<User>({
    queryKey: ['user', email],
    queryFn: () => getUser(email),
    enabled: !!email,
    placeholderData: placeholderUser,
  });
};


// const idAtom = atom("1")
// export const [userAtom] = atomsWithQuery((get) => ({
//   queryKey: ['users', get(idAtom)],
//   queryFn: async ({ queryKey: [, id] }) => {
//     const res = await fetch(`https://jsonplaceholder.typicode.com/users/${id}`)
//     return res.json()
//   },
// }))

// const UserData = () => {
//   const [data] = useAtom(userAtom)
//   return <div>{JSON.stringify(data)}</div>
// }