
const url = `/api/User/Me/Bookings`;
export const getMyBookings = async () => {
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  return await fetch(url, requestOptions);
};