const url = `/api/User/Me`;
export const getMe = async () => {
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  return await fetch(url, requestOptions).then(res => {
    return res.json();
  });
};