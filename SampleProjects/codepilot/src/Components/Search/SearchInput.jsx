import React, { useState } from 'react';
import "./SearchInput.css";

const SearchBar = ({ onSearch }) => {
  const [query, setQuery] = useState('');

  const handleInputChange = (event) => {
    setQuery(event.target.value);
  };

  const handleKeyDown = (event) => {
    if (event.key === 'Enter') {
      onSearch(query);
    }
  };

  return (
    <div className='input-container'>
      <input
        type="text"
        value={query}
        onChange={handleInputChange}
        onKeyDown={handleKeyDown}
        className='search-input'
      />
      {!query && (
        <div className="placeholder">
            <span>Search...</span>
            <svg xmlns="http://www.w3.org/2000/svg" width="11" height="11" viewBox="0 0 11 11" fill="none">
                <circle cx="4" cy="4" r="3.5" fill="white" stroke="#A09A9A"/>
                <line x1="6.35355" y1="6.64645" x2="10.3536" y2="10.6464" stroke="#A09A9A"/>
            </svg>
        </div>
      )}
    </div>
  );
};

export default SearchBar;
