import React, { useState } from 'react';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';

const CollapsibleList = () => {
  const [openIndexes, setOpenIndexes] = useState({});

  const toggleItem = (index) => {
    setOpenIndexes((prev) => ({
      ...prev,
      [index]: !prev[index]
    }));
  };

  const items = [
    { id: 1, title: 'Item 1', content: 'This is the content of item 1' },
    { id: 2, title: 'Item 2', content: 'This is the content of item 2' },
    { id: 3, title: 'Item 3', content: 'This is the content of item 3' }
  ];

  return (
    <div>
      {items.map((item, index) => (
        <div key={item.id}>
          <div
            onClick={() => toggleItem(index)}
            style={{
              cursor: 'pointer',
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              background: '#f1f1f1',
              padding: '10px',
              marginBottom: '5px'
            }}
          >
            <span>{item.title}</span>
            <span>
              {openIndexes[index] ? <FaChevronUp /> : <FaChevronDown />}
            </span>
          </div>
          {openIndexes[index] && (
            <div style={{ padding: '10px', background: '#e1e1e1' }}>
              {item.content}
            </div>
          )}
        </div>
      ))}
    </div>
  );
};

export default CollapsibleList;
