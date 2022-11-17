import React, { useEffect, useState } from 'react';
import members from './../data/members.json';
import Button from './Button';

type User = {
  id: number;
  name: string;
  isSignedIn: boolean;
  officeId: number;
};

const List = () => {
  const [users, setUsers] = useState<User[]>([]);

  const url = 'http://localhost:7081/api/User';

  useEffect(() => {
    fetch(url)
      .then(response => response.json())
      .then(data => setUsers(data));
  }, []);
  return (
    <div>
      <ul>
        {users.map(user => (
          <Button key={user.id} name={user.name} />
        ))}
      </ul>
    </div>
  );
};

export default List;
