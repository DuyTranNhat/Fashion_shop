import React from 'react';

type Props = {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  onPageChange: (page: number) => void;
};

const Paging = ({ currentPage, itemsPerPage, totalItems, onPageChange }: Props) => {
  const totalPages = Math.ceil(totalItems / itemsPerPage);

  const handleClick = (page: number) => {
    if (page > 0 && page <= totalPages) {
      onPageChange(page);
    }
  };

  return (
    <div className="col-12">
      <div>
        <ul className="pagination justify-content-center">
          <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
            <a className="page-link" href="#" onClick={() => handleClick(currentPage - 1)}>
              Previous
            </a>
          </li>
          {[...Array(totalPages)].map((_, index) => (
            <li key={index} className={`page-item ${currentPage === index + 1 ? 'active' : ''}`}>
              <a className="page-link" href="#" onClick={() => handleClick(index + 1)}>
                {index + 1}
              </a>
            </li>
          ))}
          <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
            <a className="page-link" href="#" onClick={() => handleClick(currentPage + 1)}>
              Next
            </a>
          </li>
        </ul>
      </div>
    </div>
  );
};

export default Paging;
